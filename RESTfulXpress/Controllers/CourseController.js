const CourseModel = require("../Models/CourseModel");
const FacultyModel = require("../Models/FacultyModel");

module.exports.CreateCourse = async (req, res) => {

    try {
        const courseReq = req.body;

        const facultyData = await FacultyModel.findOne({ FacultyId: courseReq.FacultyId });
        courseReq.Faculty = facultyData;

        const courseData = await CourseModel.create(courseReq);

        if (!courseData) {
            return res.status(401).json({ message: "Course create failed", success: false })
        } else {
            return res.status(201).json({ message: "Course created", success: true, course: courseData })
        }
    } catch (error) {
        return res.json({ message: "An error occured", errorcode: error.code.toString(), errorname: error.name.toString(), error: error.message.toString() });
    }
}

module.exports.GetCourses = async (req, res) => {

    try {
        const coursesData = await CourseModel.find().populate('Faculty');

        if (!coursesData) {
            return res.status(401).json({ message: "Course get failed", success: false })
        } else {
            return res.status(200).json({ message: "Courses get", success: true, courses: coursesData });
        }

    } catch (error) {
        return res.json({ message: "An error occured", errorcode: error.code.toString(), errorname: error.name.toString(), error: error.message.toString() });
    }

}

module.exports.GetCourse = async (req, res) => {

    try {

        const courseId = req.params.courseId;

        const courseData = await CourseModel.findOne({ CourseId: courseId }).populate('Faculty');

        if (!courseData) {
            return res.status(404).json({ message: "Course not found", success: false });
        }

        return res.status(200).json({ message: "Faculty get", success: true, course: courseData });

    } catch (error) {
        return res.json({ message: "An error occured", errorcode: error.code.toString(), errorname: error.name.toString(), error: error.message.toString() });
    }

}

module.exports.EditCourse = async (req, res) => {

    try {

        const courseId = req.params.courseId;
        const updatedCourseData = req.body;

        const facultyData = await FacultyModel.findOne({ FacultyId: updatedCourseData.FacultyId });
        updatedCourseData.Faculty = facultyData;

        const updatedCourse = await CourseModel.findOneAndUpdate({ CourseId: courseId }, updatedCourseData, { new: true });

        if (!updatedCourse) {
            return res.status(404).json({ message: "Course not found", success: false });
        }

        return res.status(200).json({ message: "Course updated successfully", success: true, course: updatedCourse });

    } catch (error) {
        return res.json({ message: "An error occured", errorcode: error.code.toString(), errorname: error.name.toString(), error: error.message.toString() });
    }
}

module.exports.DeleteCourse = async (req, res) => {

    try {

        const courseId = req.params.courseId;

        const deletedCourse = await CourseModel.findOneAndDelete({ CourseId: courseId });

        if (!deletedCourse) {
            return res.status(404).json({ success: false, message: "Course not found" });
        }

        return res.status(200).json({ success: true, message: "Faculty deleted successfully" });

    } catch (error) {
        return res.json({ message: "An error occured", errorcode: error.code.toString(), errorname: error.name.toString(), error: error.message.toString() });
    }
}