const { CreateClassSession, GetClassSessions, GetClassSession, EditClassSession, DeleteClassSession } = require("../Controllers/ClassSessionController");
const { verifyToken } = require("../Util/TokenHandler");
const router = require("express").Router();

router.post("/create-classsession", verifyToken, CreateClassSession);
router.get("/get-classsessions", verifyToken, GetClassSessions);
router.get("/get-classsession/:classSessionId", verifyToken, GetClassSession);
router.put("/edit-classsession/:classSessionId", verifyToken, EditClassSession);
router.delete("/delete-classsession/:classSessionId", verifyToken, DeleteClassSession);

module.exports = router;