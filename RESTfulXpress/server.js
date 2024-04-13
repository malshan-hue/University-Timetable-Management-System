const express = require('express');
const cors = require('cors');
const mongoose = require('mongoose'); 

require('dotenv').config(); 

const authRoute = require("./Routes/AuthRoute");
const facultyRoute = require("./Routes/FacultyRoutes");
const courseRoute = require("./Routes/CourseRoutes");
const classSessionRoute = require("./Routes/ClassSessionRoutes");

const app = express();
const port = process.env.PORT;

app.use(cors());
app.use(express.json());

const uri = process.env.ATLAS_URI;
mongoose.connect(uri);

const connection = mongoose.connection;
connection.once('open', () => {
    console.log("MongoDB database connection established successfully")
});

app.use("/", authRoute, facultyRoute, courseRoute, classSessionRoute);

app.listen(port, () => {
    console.log(`Server is running on port: ${port}`);
});