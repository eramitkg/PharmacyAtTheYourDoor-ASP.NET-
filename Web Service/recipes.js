const sql = require('mssql');
const express = require('express');
const router = express();   
const sqlConfig = require("./routes/dbConfig");

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