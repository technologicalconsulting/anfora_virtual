import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { login } from '../../services/authService';
import logo from '../../assets/images/logo.png';
import bg from '../../assets/images/bg-login.jpg';
import texto from '../../assets/images/Letra.png';

const LoginPage = () => {
  const [usuario, setUsuario] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    const user = await login(usuario, password);
    if (user) {
      navigate('/dashboard');
    } else {
      setError('Usuario o contraseña incorrectos');
    }
  };

  return (
    <div
      className="d-flex justify-content-center align-items-center"
      style={{
        minHeight: '100vh',
        width: '100vw',
        backgroundImage: `url(${bg})`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
        backgroundRepeat: 'no-repeat',
      }}
    >
      <div
        className="card shadow-lg p-4 rounded"
        style={{
          maxWidth: '400px',
          width: '100%',
          backgroundColor: 'rgba(255, 255, 255, 0.95)',
        }}
      >
        <div className="d-flex align-items-center justify-content-center gap-3 mb-4">
          <img src={logo} alt="Logo" style={{ height: '70px' }} />
          <img src={texto} alt="Logo" style={{ height: '50px' }} />
        </div>

        <form onSubmit={handleSubmit}>
          <div className="mb-3">
          <p className="text-center">Inicia sesión con tu usuario y contraseña</p>
            <label className="form-label fw-bold">Usuario *</label>
            <input
              type="text"
              className="form-control form-control-lg"
              value={usuario}
              onChange={(e) => setUsuario(e.target.value)}
              required
            />
          </div>

          <div className="mb-3">
            <label className="form-label fw-bold">Contraseña *</label>
            <input
              type="password"
              className="form-control form-control-lg"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>

          <div className="form-check mb-3">
            <input type="checkbox" className="form-check-input" id="rememberMe" />
            <label className="form-check-label" htmlFor="rememberMe">
              Recordar mis preferencias
            </label>
          </div>

          {error && <div className="alert alert-danger py-1">{error}</div>}

          <div className="d-grid mt-3">
            <button type="submit" className="btn btn-success btn-lg">
              Iniciar sesión
            </button>
          </div>

          {/* Enlace para recuperar contraseña */}
          <div className="text-center mt-3">
          <a href="/reset-password" className="custom-link">¿Olvidaste tu contraseña?</a>
          </div>

          {/* Registro */}
          <div className="text-center mt-3">
            
            <span>¿No tienes una cuenta? </span>
            <a href="/register" className="custom-link">Regístrate</a>

          </div>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;
