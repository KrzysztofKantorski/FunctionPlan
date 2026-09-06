/** @type {import('tailwindcss').Config} */
module.exports = {
  // 1. Tailwind przeszuka te pliki w poszukiwaniu klas
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {

      //Use variables from themes
      colors: {
        primary: 'var(--mat-sys-primary)',
        'on-primary': 'var(--mat-sys-on-primary)',
        'primary-container': 'var(--mat-sys-primary-container)',
        'on-primary-container': 'var(--mat-sys-on-primary-container)',
        secondary: 'var(--mat-sys-secondary)',
        'on-secondary': 'var(--mat-sys-on-secondary)',
        tertiary: 'var(--mat-sys-tertiary)',
        'on-tertiary': 'var(--mat-sys-on-tertiary)',
        error: 'var(--mat-sys-error)',
        'on-error': 'var(--mat-sys-on-error)',
        background: 'var(--mat-sys-background)',
        'on-background': 'var(--mat-sys-on-background)',
        surface: 'var(--mat-sys-surface)',
        'on-surface': 'var(--mat-sys-on-surface)',
        'surface-variant': 'var(--mat-sys-surface-variant)',
        'on-surface-variant': 'var(--mat-sys-on-surface-variant)',
        outline: 'var(--mat-sys-outline)',
      },


      //Fonts
      fontFamily: {

        //Defalut font
        sans: ['Inter', 'sans-serif'], 

        //Header font
        display: ['Plus Jakarta Sans', 'sans-serif'], 
      }
    },
  },
  plugins: [
    
  ],
}