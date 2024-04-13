const CourseModel = require("../Models/CourseModel");
const FacultyModel = require("../Models/FacultyModel");
const ClassSessionModel = require("../Models/ClassSessionModel");

module.exports.CreateClassSession = async (req, res) => {

    try {
        const classSessionReq = req.body;

        const facultyData = await FacultyModel.findOne({ FacultyId: classSessionReq.FacultyId });
        classSessionReq.Faculty = facultyData;

        const courseData = await CourseModel.findOne({ CourseId: classSessionReq.CourseId });
        classSessionReq.Course = courseData;

        const classSessionData = await ClassSessionModel.create(classSessionReq);

        if (!classSessionData) {
            return res.status(401).json({ message: "Class Session create failed", success: false })
        } else {
            return res.status(201).json({ message: "Course class session", success: true, classSession: classSessionData })
        }
    } catch (error) {
        return res.json({ message: "An error occured", error: error.message.toString() });
    }
}

module.exports.GetClassSessions = async (req, res) => {

    try {
        const classSessionData = await ClassSessionModel.find()
            .populate('Faculty')
            .populate({
                path: 'Course',
                populate: { path: 'Faculty' }
            });

        if (!classSessionData) {
            return res.status(401).json({ message: "Course get failed", success: false })
        } else {
            return res.status(200).json({ message: "Courses get", success: true, classSessions: classSessionData });
        }

    } catch (error) {
        return res.json({ message: "An error occured", error: error.message.toString() });
    }

}

module.exports.GetClassSession = async (req, res) => {

    try {

        const classSessionId = req.params.classSessionId;

        const classSessionData = await ClassSessionModel.findOne({ ClassSessionId: classSessionId })
            .populate('Faculty')
            .populate({
                path: 'Course',
                populate: { path: 'Faculty' }
            });

        if (!classSessionData) {
            return res.status(404).json({ message: "Class session not found", success: false });
        }

        return res.status(200).json({ message: "Faculty get", success: true, classSession: classSessionData });

    } catch (error) {
        return res.json({ message: "An error occured", error: error.message.toString() });
    }

}

module.exports.EditClassSession = async (req, res) => {
    try {
        const classSessionId = req.params.classSessionId;
        const updatedClassSessionData = req.body;

        console.log(classSessionId);
        console.log(updatedClassSessionData);

        updatedClassSessionData.Faculty = mongoose.Types.ObjectId(updatedClassSessionData.FacultyId);
        updatedClassSessionData.Course = mongoose.Types.ObjectId(updatedClassSessionData.CourseId);

        const updatedClassSession = await ClassSessionModel.findOneAndUpdate(
            { ClassSessionId: classSessionId },
            updatedClassSessionData,
            { new: true }
        );

        console.log(updatedClassSession);

        if (!updatedClassSession) {
            return res.status(404).json({ message: "Class session not found", success: false });
        }

        return res.status(200).json({ message: "Course updated successfully", success: true, classSession: updatedClassSession });
    } catch (error) {
        return res.json({ message: "An error occurred", error: error.message.toString() });
    }
}


module.exports.DeleteClassSession = async (req, res) => {

    try {

        const classSessionId = req.params.classSessionId;

        const deletedClassSession = await ClassSessionModel.findOneAndDelete({ ClassSessionId: classSessionId });

        if (!deletedClassSession) {
            return res.status(404).json({ success: false, message: "class session not found" });
        }

        return res.status(200).json({ success: true, message: "Class session deleted successfully" });

    } catch (error) {
        return res.json({ message: "An error occured", error: error.message.toString() });
    }
}