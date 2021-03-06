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
            .input('PharmacyID', req.body.PharmacyID)
            .input('IsDelivered', req.body.IsDelivered)
            .execute('GetMedicineForPharmacy')
    }).then(result =>{
        res.send(result.recordsets[0]);
    })  
})

router.post("/getmedicinesfordoctor",(req,res,next)=>{
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{  
            return pool.request()
            .input('doctorId', req.body.doctorId)
            .execute("GetMedicineForDoctor")
    }).then(result =>{
        res.send(result.recordsets[0]);
    })  
});

router.post("/createrecipefordoctor",(req,res,next)=>{
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{  
            return pool.request()
            .input('tcNo', req.body.tcNo)
            .input('doctorId', req.body.doctorId)
            .execute("CreateRecipeForDoctor")
    }).then(result =>{
        res.send(result.returnValue.toString());
    })
});

router.post("/addmedicinetorecipefordoctor",(req,res,next)=>{
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{  
            return pool.request()
            .input('medName', req.body.medName)
            .input('medType', req.body.medType)
            .input('medUsage', req.body.medUsage)
            .execute("AddMedicineToRecipeForDoctor")
    }).then(result =>{
        res.send(result.recordsets[0]);
    })
});

router.post("/recipeDeliver",(req,res,next)=>{
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{  
            return pool.request()
            .input('RecipeID', req.body.RecipeID)
            .execute("RecipeDeliver")
    }).then(result =>{
        res.send(result.returnValue.toString());
    })
});


module.exports = router;