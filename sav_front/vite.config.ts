import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    host: '0.0.0.0', // 👈 esto permite acceso desde la red
    port: 5173       // (opcional) asegúrate de usar el puerto correcto
  }
})
