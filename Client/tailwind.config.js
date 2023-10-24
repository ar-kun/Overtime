/** @type {import('tailwindcss').Config} */
module.exports = {
 content: ['./Pages/**/*.cshtml', './Views/**/*.cshtml'],
 darkMode: 'class',
 theme: {
  container: {
   center: true,
   padding: '18px',
  },
  extend: {
   colors: {
    primary: '#fafbfa',
    secondary: '#5874a7',
    third: '#163270',
   },
   fontFamily: {
    primary: ['Jomolhari', 'serif'],
    secondary: ['jsMath-cmmi10', 'sans-serif'],
   },
  },
 },
};
