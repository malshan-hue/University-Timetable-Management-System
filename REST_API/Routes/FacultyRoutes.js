const { CreateFaculty, GetFaculties, GetFaculty, EditFaculty, DeleteFaculty } = require("../Controllers/FacultyController");
const { verifyToken } = require("../Util/TokenHandler");
const router = require("express").Router();

router.post("/create-faculty", verifyToken, CreateFaculty);
router.get("/get-faculties", verifyToken, GetFaculties);
router.get("/get-faculty/:facultyId", verifyToken, GetFaculty);
router.put("/edit-faculty/:facultyId", verifyToken, EditFaculty);
router.delete("/delete-faculty/:facultyId", verifyToken, DeleteFaculty);

module.exports = router;