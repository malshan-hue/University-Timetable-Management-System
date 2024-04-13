const { SignUp, Login, Welcome } = require("../Controllers/AuthController");
const router = require("express").Router();

router.post('/signup', SignUp);
router.post('/login', Login);
router.get('/', Welcome);

module.exports = router;