module.exports = {
  mode: 'jit',
  purge: {
    enabled: true,
    content: [
        './*.html'
    ]
},
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        blue:{
          brand: '#3B53DB',
          light: '#0EB7FE',
          navy: '#20224B',
          dark: '#141631'
        },
        dark:{
          brand: '#262626',
          mid: '#787878',
          gray: '#969696',
          light: '#C3C3C3',
          fural: '#F1F1F1',
          white: '#EBEDFB'
        },
        red: {
          brand: '#E44452',
        },
        orange: {
          brand: '#F1682C'
        }
      }
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
