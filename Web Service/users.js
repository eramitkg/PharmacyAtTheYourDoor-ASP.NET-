const sql = require('mssql');
const express = require('express');
const router = express();   
const sqlConfig = require("./routes/dbConfig");

router.post("/login",(req,res,next)=>{
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{
        return pool.request()
        .input('TCNo',req.body.TCNo)
        .input('Password',req.body.Password)
        .execute("Login")
    }).then(result =>{
        res.send(JSON.stringify(result.returnValue));
    })
})
router.post("/register",(req,res,next) =>{
    res.send("Register Page");
})
module.exports = router;