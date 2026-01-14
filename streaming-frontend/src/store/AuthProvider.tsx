import axios from "axios"
import { createContext, useState } from "react"


interface LoggedinUser{
    Name:string;
    UserName:string;
    Role:string;
}
interface AuthContextValue{
    userPrincipal:LoggedinUser|null;
    login:(username:string,password:string)=>Promise<void>;
    logout:()=>Promise<void>;
    
}

const AuthContext=createContext<AuthContextValue>({} as AuthContextValue)
export default function AuthenticationProvider(
    child:React.ReactNode
){

    
    const [userPrincipal,setUserPrincipal]=useState<LoggedinUser>({} as LoggedinUser)

    const login= async (username:string,password:string)=>{
        await axios.post(
            'http//localhost:8000/login',
            {
                username:username,
                password:password
            }
        )
        .then(
            (Response)=>{
                setUserPrincipal(Response.data.LoggedinUser)
            }
        )
    }
    const logout=async()=>{
        await axios.post(
            'http//localhost:8000/logout'
        )
        setUserPrincipal({} as unknown as LoggedinUser)
    }

    

    return (
        <AuthContext.Provider value={{userPrincipal, login ,logout}}>
            {child}
        </AuthContext.Provider>
    )

    
}