const { CreateCourse, GetCourses, GetCourse, EditCourse, DeleteCourse } = require("../Controllers/CourseController");
const { verifyToken } = require("../Util/TokenHandler");
const router = require("express").Router();

router.post("/create-course", verifyToken, CreateCourse);
router.get("/get-courses", verifyToken, GetCourses);
router.get("/get-course/:courseId", verifyToken, GetCourse);
router.put("/edit-course/:courseId", verifyToken, EditCourse);
router.delete("/delete-course/:courseId", verifyToken, DeleteCourse);

module.exports = router;