/**
 * Tailwind CSS Configuration
 * ---------------------------
 * This configuration defines the content sources, theme customizations,
 * and additional plugins for your Tailwind CSS setup.
 */
/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './*.html',           // Include all HTML files
        './**/*.razor',          // Include all Blazor `.razor` files
        './wwwroot/**/*.css',    // Include CSS from the static wwwroot directory
    ],
    theme: {
        extend: {
            // Custom Color Palette
            colors: {
                primary: '#3490dc',  // Blue for primary actions
                secondary: '#ffed4a', // Yellow for highlights
                danger: '#e3342f',    // Red for errors or destructive actions
                success: '#38c172',   // Green for success states
                info: '#6cb2eb',      // Light blue for informational elements
                warning: '#f6ad55',   // Orange for warnings
            },
            // Custom Fonts
            fontFamily: {
                sans: ['Inter var', 'sans-serif'], // Use Inter for modern, readable sans-serif
                mono: ['Roboto Mono', 'monospace'], // Use Roboto Mono for monospace needs
            },
        },
    },
    plugins: [
        require('@tailwindcss/typography'), // Plugin for rich text formatting
        require('@tailwindcss/forms'),     // Plugin for styling forms
        require('@tailwindcss/aspect-ratio') // Plugin for controlling aspect ratios
    ],
};
