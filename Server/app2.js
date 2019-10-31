const sql = require('mssql');
const express = require('express');

const app = express();

var sqlConfig = {
    server: '127.0.0.1',
    database: 'KapinizdakiEczane',
    user: 'oguzhankaymak',
    password: '12345',
};

 

function getBlog(){
     
    var conn = new sql.ConnectionPool(sqlConfig);
    var req = new sql.Request(conn);
     
    conn.connect(function(error){
         
        if(error){
             
            console.log(err);
            return;
        }
         
        req.query("SELECT * FROM Pharmacy",function(err, redocrset){
            if(err)
            {
                 
            }
            else{
                console.log(redocrset)
                return redocrset;
            }
             
        })
         
    })
     
}
 
var deneme = getBlog();
console.log(deneme);
