const mongoose = require('mongoose');

const courseSchema = new mongoose.Schema({
    CourseId: String,
    FacultyId: String,
    CourseCode: String,
    CourseName: String,
    CourseDescription: String,
    CourseCredit: Number,
    IsDeleted: Boolean,
    Faculty: { type: mongoose.Schema.Types.ObjectId, ref: 'FacultyModel' }
});

module.exports = mongoose.model('CourseModel', courseSchema);