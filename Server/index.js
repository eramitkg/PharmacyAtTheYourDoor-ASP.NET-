var sql = require('mssql');
var express = require('express');

const app = express();
var sqlConfig = {
    server: '127.0.0.1',
    database: 'Northwind',
    user: 'oguzhankaymak',
    password: '12345',
};

var nakliyeciler;
( async function() {
    try {
        console.log("sql connecting......")
        let pool = await sql.connect(sqlConfig)
        let result = await pool.request()
            .query('select * from Nakliyeciler')  // subject is my database table name

        nakliyeciler = result;
        console.log(nakliyeciler)
        

    } catch (err) {
        console.log(err);
    }
})()


app.get("/",(req,res,next) =>{
    res.send(nakliyeciler);
    
});
app.listen(3000)
