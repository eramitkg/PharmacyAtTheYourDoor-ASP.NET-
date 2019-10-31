var express = require('express');
const sql = require('mssql')

const app = express();
var sqlConfig = {
    server: '127.0.0.1',
    database: 'Northwind',
    user: 'oguzhankaymak',
    password: '12345',
};

var deneme;
async function messageHandler(){
    try {
        console.log("Sql connection...")
        let pool = await sql.connect(sqlConfig)
        
        let result = await pool.request()
        .input('ID',1)
        .execute('GetValues')
        deneme = result.recordsets[0];
        console.log(result.recordsets[0]);
    } 
    catch (err) {
       console.log("olmadı")
    }
}
messageHandler()
console.log("----deneme----1")
console.log("----deneme----2")
console.log("----deneme----3")

app.listen(3000);
app.get("/",(res,req,next) => {
    req.send(deneme);
})

