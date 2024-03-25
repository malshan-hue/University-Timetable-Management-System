const FacultyModel = require("../Models/FacultyModel");

module.exports.CreateFaculty = async (req, res) => {

    try {
        const facultyReq = req.body;

        const facultyData = await FacultyModel.create(facultyReq);

        if (!facultyData) {
            return res.status(401).json({ message: "Faculty create failed", success: false })
        } else {
            return res.status(201).json({ message: "Faculty created", success: true, faculty: facultyData })
        }
    } catch (error) {
        return res.json({ message: "An error occured", errorcode: error.code, errorname: error.name });
    }
}

module.exports.GetFaculties = async (req, res) => {

    try {
        const facultiesData = await FacultyModel.find();

        if (!facultiesData) {
            return res.status(401).json({ message: "Faculty get failed", success: false })
        } else {
            return res.status(200).json({ message: "Faculties get", success: true, faculties: facultiesData });
        }

    } catch (error) {
        return res.status(500).json({ message: "An error occurred", error: error.message });
    }

}

module.exports.GetFaculty = async (req, res) => {

    try {

        const facultyId = req.params.facultyId;

        const facultyData = await FacultyModel.findOne({ FacultyId: facultyId });

        if (!facultyData) {
            return res.status(404).json({ message: "Faculty not found", success: false });
        }

        return res.status(200).json({ message: "Faculty get", success: true, faculty: facultyData });

    } catch (error) {
        return res.status(500).json({ message: "An error occurred", error: error.message });
    }
    
}

module.exports.EditFaculty = async (req, res) => {

    try {

        const facultyId = req.params.facultyId;
        const updatedFacultyData = req.body; 
        
        const updatedFaculty = await FacultyModel.findOneAndUpdate({ FacultyId: facultyId }, updatedFacultyData, { new: true });


        if (!updatedFaculty) {
            return res.status(404).json({ message: "Faculty not found", success: false });
        }

        return res.status(200).json({ message: "Faculty updated successfully", success: true, faculty: updatedFaculty });

    } catch (error) {
        console.error("Error fetching faculty:", error);
        return res.status(500).json({ message: "An error occurred", error: error.message });
    }
}

module.exports.DeleteFaculty = async (req, res) => {

    try {

        const facultyId = req.params.facultyId;

        const deletedFaculty = await FacultyModel.findOneAndDelete({ FacultyId: facultyId });

        if (!deletedFaculty) {
            return res.status(404).json({ success: false, message: "Faculty not found" });
        }

        return res.status(200).json({ success: true, message: "Faculty deleted successfully" });

    } catch (error) {
        return res.status(500).json({ message: "An error occurred", error: error.message });
    }
}