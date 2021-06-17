module.exports = {
    purge: {
        enabled: false,
        content: [
            'Views/**/*.cshtml'
        ]
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {
            border: ['focus']
        },
    },
    variants: {
        extend: {
            borderWidth: ['focus'],
        },
    },
    plugins: [],
}
