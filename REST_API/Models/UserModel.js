const mongoose = require('mongoose');

const UserRoleEnum = {
    Admin: 0,
    Faculty: 1,
    Student: 2
};

const userSchema = new mongoose.Schema({
    UserId: String,
    FirstName: String,
    LastName: String,
    Password: String,
    PasswordSalt: String,
    PersonalEmail: String,
    OfficialEmail: String,
    UserName: String,
    MobileNumber: String,
    UserRoleEnum: {
        type: Number,
        enum: Object.values(UserRoleEnum), 
        default: UserRoleEnum.Admin
    },
    UserRoleEnumDisplayname: String,
    RememberMe: Boolean
});

module.exports = mongoose.model('UserModel', userSchema);
