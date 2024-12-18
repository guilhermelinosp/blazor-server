/**
 * Tailwind CSS Configuration
 * ---------------------------
 * This configuration defines the content sources, theme customizations,
 * and additional plugins for your Tailwind CSS setup.
 */
/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './*.html',            // Include all HTML files
        './**/*.razor',        // Include all Blazor `.razor` files
        './wwwroot/**/*.css',  // Include CSS files from the wwwroot directory
        './src/**/*.{js,ts,jsx,tsx}', // If you're using JavaScript/TypeScript files
    ],
    darkMode: 'class', // Enable dark mode based on class toggling
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
                background: '#f9fafb', // Light background color for general UI
                darkBackground: '#1a202c', // Dark background color for dark mode
            },
            // Custom Fonts
            fontFamily: {
                sans: ['Inter var', 'sans-serif'], // Use Inter for modern, readable sans-serif
                mono: ['Roboto Mono', 'monospace'], // Use Roboto Mono for monospace needs
            },
            // Custom Breakpoints
            screens: {
                'xs': '480px', // Small screen size for mobile
                ...require('tailwindcss/defaultTheme').screens, // Keep default breakpoints
            },
            // Spacing (example: custom spacing for margins/paddings)
            spacing: {
                '18': '4.5rem', // Adding a custom spacing unit
                '84': '21rem',  // Another custom spacing unit
            },
        },
    },
    plugins: [
        require('@tailwindcss/typography'), // Plugin for rich text formatting
        require('@tailwindcss/forms'),     // Plugin for styling forms
        require('@tailwindcss/aspect-ratio'), // Plugin for controlling aspect ratios
        require('@tailwindcss/line-clamp'), // Add line-clamp for truncating multi-line text
    ]
};
