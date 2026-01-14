import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";


function LoginComponent(){
    const [userName,setuserName]=useState<string>("")
    const [password,setPassword]=useState<string>("")
    const navigate=useNavigate()

    async function Login(){
        const username=userName
        const Password=password
        try{
            
            
            
            navigate("/")
        }
        catch(error){
            console.log(error)
        }
        
    }

    return (
        <div className="LoginContainer">
            <div className="LoginComponent">
                <label>UserName</label>
                <input className="CredInput" type="email" value={userName} onChange={(e)=>setuserName(e.target.value)}></input>
                <label>Pasword</label>
                <input className="CredInput" type="password" value={password} onChange={(e)=>setPassword(e.target.value)}></input>
                <button onClick={Login}>Login</button>
            </div>
        </div>
    );
}

export default LoginComponent