#!/bin/bash
# Runs once, automatically, the first time the primary's data directory is
# initialized (standard docker-entrypoint-initdb.d behavior - it will NOT
# re-run on subsequent container restarts since PGDATA already exists then).
set -euo pipefail

echo "[init-replication] creating replication role and slot"

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    CREATE ROLE ${REPLICATION_USER} WITH REPLICATION LOGIN PASSWORD '${REPLICATION_PASSWORD}';
    SELECT pg_create_physical_replication_slot('replica_slot');
EOSQL

# Dev-only: allow the replication role to connect from anywhere on the compose
# network. In a real environment you'd scope this to the replica's actual
# address/subnet instead of 0.0.0.0/0.
echo "host replication ${REPLICATION_USER} 0.0.0.0/0 md5" >> "$PGDATA/pg_hba.conf"

echo "[init-replication] done"
