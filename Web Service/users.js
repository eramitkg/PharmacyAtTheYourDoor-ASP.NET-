const sql = require('mssql');
const express = require('express');
const router = express();
const sqlConfig = require("./routes/dbConfig");

router.post("/getpatientsforpharmacy", (req, res, next) => {

    new sql.ConnectionPool(sqlConfig).connect()
        .then(pool => {
            return pool.request()
                .input('PharmacyId', req.body.PharmacyId)
                .execute("GetPatientsForPharmacy")

        }).then(result => {
            res.send(result.recordsets[0]);
        })
})

router.post("/login", (req, res, next) => {

    new sql.ConnectionPool(sqlConfig).connect()
        .then(pool => {

            if (req.body.Role == "doctor") {
                return pool.request()
                    .input('TCNo', req.body.TCNo)
                    .input('Password', req.body.Password)
                    .execute("LoginDoctor")
            }
            else if (req.body.Role == "patient") {
                return pool.request()
                    .input('TCNo', req.body.TCNo)
                    .input('Password', req.body.Password)
                    .execute("LoginPatient")
            }

            else {
                return pool.request()
                    .input('RecordNo', req.body.RecordNo)
                    .input('Password', req.body.Password)
                    .execute("LoginPharmacy")
            }

        }).then(result => {
            res.send(result.recordsets[0]);
        })
})
router.post("/register", (req, res, next) => {
    res.send("Register Page");
})

router.post("/getPatientInformation",(req,res,next)=>{
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{  
            return pool.request()
            .input('PatientID', req.body.PatientID)
            .execute("GetPatientInformation")
    }).then(result =>{
        res.send(result.recordsets[0]);
    })
}); 

router.get("/getPharmacies",(req,res,next)=>{
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{  
            return pool.request()
            .execute("GetPharmacies")
    }).then(result =>{
        res.send(result.recordsets[0]);
    })
});

router.post("/changePharmacy",(req,res,next)=>{
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool =>{  
            return pool.request()
            .input('PatientID', req.body.PatientID)
            .input('PharmacyID', req.body.PharmacyID)
            .execute("ChangePharmacy")
    }).then(result =>{
        res.send(result.returnValue.toString());
    })
}); 


module.exports = router;