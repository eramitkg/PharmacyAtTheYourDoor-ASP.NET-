const sql = require('mssql');
const express = require('express');
const userRouter = require('./routes/users');

var bodyParser = require('body-parser');


const PORT = 5000 || process.env.PORT;
const app = express();

const sqlConfig = require("./routes/dbConfig");

app.use(bodyParser.json()); // support json encoded bodies
app.use(bodyParser.urlencoded({ extended: true })); // support encoded bodies

app.use(userRouter)

app.get("/", (req, res, next) => {
    res.send("Oldu");
})
app.get("/getPharmacy", (req, res, next) => {
    new sql.ConnectionPool(sqlConfig).connect()
    .then(pool => {
        return pool.request().query('select * from Pharmacy')
    }).then(result => {
        res.send(result.recordset)
        sql.close()
    }).catch(err=> {
        res.status(500).send({ message: `${err}` })
        sql.close();
    })
})
app.post('/api/users', function(req, res) {
    var user_id = req.body.id;
    var token  = req.body.token;
    var geo = req.body.geo;
    var bearer = req.body.Bearer;
    console.log(geo);
    console.log(token);
    res.send("oldu");
});

app.use((req, res, next) => {
    res.send("404 NOT FOUND");
})
app.listen(PORT);