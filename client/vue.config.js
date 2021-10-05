module.exports = {
    outputDir: '../server/src/wwwroot',
    css: {
        extract: true
    },
    pages: {
        signIn: {
            entry: 'src/apps/sign-in/main.js',
            template: 'public/sign-in.html',
            filename: 'index.html',
            title: 'Sign In',
        },
        /*students: {
            entry: 'src/apps/students/main.js',
            template: 'public/students.html',
            filename: 'students.html',
            title: 'Students',
        }*/
    }
}