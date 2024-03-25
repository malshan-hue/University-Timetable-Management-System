const app = require('../server'); // Import your server app
const request = require('supertest'); // Import SuperTest
const UserModel = require('../Models/UserModel'); // Import UserModel

// Clean up test database before each test
beforeEach(async () => {
    await UserModel.deleteMany({});
});

test('Login with valid username should return success and user data', async () => {
    // Create a test user in the database
    const testUser = await UserModel.create({
        UserName: 'malshan.rathnayake@sliit.lk',
        // Other required fields...
    });

    console.log('Sending request to:', '/auth/login');
    const response = await request(app)
        .post('/auth/login')
        .send({ UserName: testUser.UserName });
    console.log('Received response:', response.status, response.body);



    // Assert the response status code and content
    expect(response.status).toBe(200);
    expect(response.body).toEqual({
        message: 'User Logged Successfully',
        success: true,
        user: testUser
    });
});
