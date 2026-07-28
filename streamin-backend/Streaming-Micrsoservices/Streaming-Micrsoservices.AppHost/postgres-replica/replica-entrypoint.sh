#!/bin/bash
# On first run (empty PGDATA), this clones the primary via pg_basebackup and
# marks the result as a standby (-R writes standby.signal + primary_conninfo).
# On every subsequent restart, PGDATA is already populated, so this block is
# skipped entirely and we just hand off to the normal postgres entrypoint -
# exactly like a real self-hosted replica would behave.
set -euo pipefail

if [ -z "$(ls -A "$PGDATA" 2>/dev/null)" ]; then
    echo "[replica-entrypoint] PGDATA is empty - waiting for the primary"

    until pg_isready -h "$PRIMARY_HOST" -U "$REPLICATION_USER" >/dev/null 2>&1; do
        echo "[replica-entrypoint] primary not reachable yet, retrying in 2s"
        sleep 2
    done

    echo "[replica-entrypoint] primary is up, running pg_basebackup"

    gosu postgres bash -c "
        PGPASSWORD='$REPLICATION_PASSWORD' pg_basebackup \
            -h '$PRIMARY_HOST' \
            -D '$PGDATA' \
            -U '$REPLICATION_USER' \
            -Fp -Xs -P -R \
            --slot=replica_slot
    "

    # pg_basebackup -R writes primary_conninfo WITHOUT a password. Persist one
    # via .pgpass so the standby can keep re-authenticating to the primary
    # across reconnects (e.g. after the primary restarts).
    gosu postgres bash -c "
        echo '$PRIMARY_HOST:5432:*:$REPLICATION_USER:$REPLICATION_PASSWORD' > ~/.pgpass
        chmod 600 ~/.pgpass
    "

    echo "[replica-entrypoint] base backup complete"
fi

echo "[replica-entrypoint] starting postgres in standby mode"
exec docker-entrypoint.sh postgres -c hot_standby=on
