require("dotenv").config();
const jwt = require("jsonwebtoken");

module.exports.createToken = (userData) => {
    const payload = {
        id: userData._id,
        userName: userData.UserName,
        role: userData.UserRoleEnum
    };
    return jwt.sign(payload, process.env.TOKEN_KEY, { expiresIn: '3d' });
};

module.exports.verifyToken = (req, res, next) => {
    const token = req.header('Authorization');

    if (!token) {
        return res.status(401).json({ message: 'Authorization denied. No token provided.' });
    }

    try {
        const decoded = jwt.verify(token, process.env.TOKEN_KEY);
        req.user = decoded;
        next();
    } catch (error) {
        return res.status(401).json({ message: 'Token is not valid.' });
    }
};