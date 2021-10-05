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
            title: 'Todo App - Sign In',
        },
        app: {
            entry: 'src/apps/app/main.js',
            template: 'public/app.html',
            filename: 'app.html',
            title: 'Todo App',
        }
    }
}