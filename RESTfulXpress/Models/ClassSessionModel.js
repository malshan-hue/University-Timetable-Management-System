const mongoose = require('mongoose');

const classSessionSchema = new mongoose.Schema({
    ClassSessionId: String,
    FacultyId: String,
    CourseId: String,
    SessionDateTime: String,
    LocationEnum: Number,
    LocationEnumDisplayname: String,
    IsDeleted: Boolean,
    Faculty: { type: mongoose.Schema.Types.ObjectId, ref: 'FacultyModel' },
    Course: { type: mongoose.Schema.Types.ObjectId, ref: 'CourseModel' }
});

module.exports = mongoose.model('ClassSessionModel', classSessionSchema);