const mongoose = require('mongoose');

const facultySchema = new mongoose.Schema({
    FacultyId: String,
    FacultyName: String,
    IsDeleted: Boolean
});

module.exports = mongoose.model('FacultyModel', facultySchema);