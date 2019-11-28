const sql = require('mssql');
const express = require('express');
const router = express();   
const sqlConfig = require("./routes/dbConfig");

router.post("/login",(req,res,next)=>{

    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{  
        console.log("TC : " + req.body.TCNo)
        console.log("PASSWORD : " + req.body.Password)
        console.log("ROLE : " + req.body.Role)
        if(req.body.Role =="doctor"){
            console.log("girdi");
            return pool.request()
            .input('TCNo',req.body.TCNo)
            .input('Password',req.body.Password)
            .execute("LoginDoctor")
        }
        else if(req.body.Role =="user"){
            return pool.request()
            .input('TCNo',req.body.TCNo)
            .input('Password',req.body.Password)
            .execute("Login")
        }

        else{
            return pool.request()
            .input('TCNo',req.body.TCNo)
            .input('Password',req.body.Password)
            .execute("LoginPharmacy")
        }
        
    }).then(result =>{
        res.send(result.recordsets[0]);
    })
})
router.post("/register",(req,res,next) =>{
    res.send("Register Page");
})
module.exports = router;