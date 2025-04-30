// src/routes/AppRouter.tsx
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import LoginPage from '../pages/Authorization/LoginPage';
import NotFound from '../pages/Errores/Error404';
import ResetPasswordPage from '../pages/Authorization/ForgotPassword'

// Rutas de ejemplo para cada submódulo del menú
const Placeholder = ({ title }: { title: string }) => (
  <div className="p-4"><h2>{title}</h2><p>Contenido en desarrollo.</p></div>
)

const AppRouter = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/reset-password" element={<ResetPasswordPage />} />
        
         {/* Fallback */}
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  );
};

export default AppRouter;
