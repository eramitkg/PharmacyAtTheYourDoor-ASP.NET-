const sql = require('mssql');
const express = require('express');
const router = express();   
const sqlConfig = require("./routes/dbConfig");

router.post("/getmedicinesforpatient",(req,res,next)=>{
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{  
            return pool.request()
            .input('PatientId', req.body.PatientId)
            .input('IsDelivered', req.body.IsDelivered)
            .execute("GetMedicineForPatient")
    }).then(result =>{
        res.send(result.recordsets[0]);
    })  
})

router.post("/getmedicinesforpharmacy",(req,res,next)=>{
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{  
            return pool.request()
            .input('PharmacyId', req.body.PharmacyId)
            .input('IsDelivered', req.body.IsDelivered)
            .execute("GetMedicineForPharmacy")
    }).then(result =>{
        res.send(result.recordsets[0]);
    })  
})

module.exports = router;