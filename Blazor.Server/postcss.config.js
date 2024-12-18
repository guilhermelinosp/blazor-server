module.exports = {
    plugins: [
        require('tailwindcss')(),
        require('autoprefixer')({
            overrideBrowserslist: ['last 2 versions', 'ie >= 11'],
        }),
        ...(process.env.NODE_ENV === 'production' ? [require('cssnano')({
            preset: 'default',
        })] : []),
    ],
    // Enable source maps for easier debugging in development mode
    sourceMap: process.env.NODE_ENV === 'development',
};
