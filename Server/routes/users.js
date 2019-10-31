const express = require('express');
const router = express();   

router.get("/login",(req,res,next) =>{
    res.send("Login Page");
})

router.post("/login",(req,res,next)=>{
    res.send("Login Attemted");
})
router.get("/register",(req,res,next) =>{
    res.send("Register Page");
})
module.exports = router;