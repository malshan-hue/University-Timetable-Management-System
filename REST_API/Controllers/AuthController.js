const UserModel = require("../Models/UserModel")
const { createToken } = require("../Util/TokenHandler");

module.exports.Login = async (req, res, next) => {

    try {
        const { UserName } = req.body;

        const userData = await UserModel.findOne({ UserName });

        if (!userData) {
            return res.status(401).json({ message: "Incorrect password or email" })
        } else {
            const token = createToken(userData);
            res.setHeader('Authorization', `Bearer ${token}`);
            /*res.cookie('usertoken', token, { httpOnly: true });*/
            return res.status(200).json({ message: "User Logged Successfully", success: true, user: userData, token: token })
        }

    } catch (error) {
        
        return res.json({ message: "An error occured", error: error.message.toString() });

    }
}

module.exports.SignUp = async (req, res, next) => {
    try {
        const user = req.body;

        const result = await UserModel.create(user);

        if (!result) {
            return res.status(401).json({ message: "user not created" })
        } else {
            const token = createToken(result);
            res.setHeader('Authorization', `Bearer ${token}`);
            /*res.cookie('usertoken', token, { httpOnly: true });*/
            return res.status(201).json({ message: "User signed up successfully", success: true, user });
        }
        next();

    } catch (error) {

        return res.json({ message: "An error occured", errorcode: error.code.toString(), errorname: error.name.toString(), error: error.message.toString() });
    }
};