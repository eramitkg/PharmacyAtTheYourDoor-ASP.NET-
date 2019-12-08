const sql = require('mssql');
const express = require('express');
const userRouter = require('./users');
const medicinesRouter = require('./medicines');

var bodyParser = require('body-parser');


const PORT = 5000 || process.env.PORT;
const app = express();



app.use(bodyParser.json()); // support json encoded bodies
app.use(bodyParser.urlencoded({ extended: true })); // support encoded bodies

app.use(userRouter)
app.use(medicinesRouter)

app.get("/", (req, res, next) => {
    res.send("Oldu");
})

app.use((req, res, next) => {
    res.send("404 NOT FOUND");
})
app.listen(PORT, ()=>{
    console.log("Yayinda : "+ PORT)
});