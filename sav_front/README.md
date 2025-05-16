# 💼 Sistema de Gestión Empresarial (SGE) - Frontend

Este es el frontend del **Sistema de Gestión Empresarial (SGE)** desarrollado por **TCR Technological Consulting & Risk S.A.S.**.  
Permite a empresas gestionar su estructura organizativa, usuarios, clientes y el control operativo del negocio.
**
---

## 🚀 Tecnologías utilizadas

- ⚛️ **React** + **TypeScript**
- ⚡ **Vite** (empaquetador ultrarrápido)
- 🎨 **CSS** para estilos personalizados
- 🧹 **ESLint** para control de calidad del código
- 🧪 Preparado para incluir pruebas con `Jest` o `React Testing Library`

---

## 📂 Estructura del proyecto

```
sge_front/
├── public/
├── src/
│   ├── assets/
│   ├── components/
│   ├── hooks/
│   ├── pages/
│   ├── router/
│   ├── services/
│   ├── utils/
│   └── App.tsx
├── .gitignore
├── index.html
├── package.json
├── tsconfig.json
└── vite.config.ts
```

---

## 🧑‍💻 Instalación y ejecución local

1. Clona el repositorio

```bash
git clone https://github.com/technologicalconsulting/sge_front.git
cd sge_front
```

2. Instala las dependencias

```bash
npm install
```

3. Ejecuta el servidor de desarrollo

```bash
npm run dev
```

4. Accede desde tu navegador:  
[http://localhost:5173](http://localhost:5173)

---

## ⚙️ Variables de entorno

Crea un archivo `.env` en la raíz del proyecto con el siguiente contenido:

```env
VITE_API_URL=https://tuservidor.com/api
```**

---

## ✅ Estado actual

- [x] Módulo de autenticación
- [x] Gestión de usuarios y empresas
- [x] Panel administrativo
- [x] Integración con backend
- [ ] Pruebas automatizadas (en progreso)

---

## 🧪 Pruebas (opcional)

Próximamente se implementarán pruebas con:

```bash
npm install --save-dev jest @testing-library/react @testing-library/jest-dom
```

---

## 🌐 Despliegue en producción

El sistema se encuentra desplegado en:  
🔗 [https://sge-front-rho.vercel.app](https://sge-front-rho.vercel.app)

---

## 📸 Capturas de pantalla

_¡Próximamente!_ Se añadirán imágenes del sistema en funcionamiento.

---

## 👨‍💼 Desarrollado por

**TCR Technological Consulting & Risk S.A.S.**  
Soluciones tecnológicas empresariales y de seguridad informática.

---

## 📬 Contacto

- 🌐 [www.tcrconsulting.ec](https://www.tcrconsulting.ec)
- ✉️ contacto@tcrconsulting.ec
