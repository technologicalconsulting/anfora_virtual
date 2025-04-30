import { useState } from 'react'
import { sendCedula, verifyCode, resetPassword } from '../../services/authService'
import bg from '../../assets/images/bg-login.jpg'
import logo from '../../assets/images/logo.png'

export default function ResetPasswordPage() {
  const [fase, setFase] = useState<'inicio' | 'verificacion' | 'nueva-clave'>('inicio')
  const [cedula, setCedula] = useState('')
  const [codigo, setCodigo] = useState('')
  const [password, setPassword] = useState('')
  const [confirmar, setConfirmar] = useState('')
  const [mensaje, setMensaje] = useState('')
  const [error, setError] = useState('')

  const handleVerificarCedula = async (e: React.FormEvent) => {
    e.preventDefault()
    setError('')
    setMensaje('')
    try {
      await sendCedula(cedula)
      setMensaje('Código enviado correctamente')
      setFase('verificacion')
    } catch {
      setError('Error al enviar la cédula')
    }
  }

  const handleVerificarCodigo = async (e: React.FormEvent) => {
    e.preventDefault()
    setError('')
    setMensaje('')
    try {
      await verifyCode(cedula, codigo)
      setFase('nueva-clave')
    } catch {
      setError('Código incorrecto')
    }
  }

  const handleCambiarClave = async (e: React.FormEvent) => {
    e.preventDefault()
    setError('')
    setMensaje('')
    if (password !== confirmar) {
      setError('Las contraseñas no coinciden')
      return
    }
    try {
      await resetPassword(cedula, password)
      setMensaje('Contraseña actualizada exitosamente')
      setFase('inicio')
      setCedula('')
      setCodigo('')
      setPassword('')
      setConfirmar('')
    } catch {
      setError('No se pudo actualizar la contraseña')
    }
  }

  const renderFormulario = () => {
    if (fase === 'inicio' || fase === 'verificacion') {
      return (
        <form onSubmit={fase === 'inicio' ? handleVerificarCedula : handleVerificarCodigo}>
          <div className="mb-3">
            <label className="form-label fw-bold">Cédula *</label>
            <input
              type="text"
              className="form-control"
              value={cedula}
              onChange={(e) => setCedula(e.target.value)}
              required
              disabled={fase === 'verificacion'}
            />
          </div>

          {fase === 'verificacion' && (
            <div className="mb-3">
              <label className="form-label fw-bold">Código de verificación *</label>
              <input
                type="text"
                className="form-control"
                maxLength={6}
                pattern="\d{6}"
                value={codigo}
                onChange={(e) => setCodigo(e.target.value)}
                required
              />
            </div>
          )}

          {error && <div className="alert alert-danger py-1">{error}</div>}
          {mensaje && <div className="alert alert-success py-1">{mensaje}</div>}

          <div className="d-grid mb-2">
            <button className="btn btn-success">
              {fase === 'inicio' ? 'Verificar' : 'Completar Registro'}
            </button>
          </div>

          <div className="text-center">
            <span>¿Tienes una cuenta? </span>
            <a href="/" className="custom-link">Iniciar sesión</a>
          </div>
        </form>
      )
    }

    return (
      <form onSubmit={handleCambiarClave}>
        <div className="mb-3">
          <label className="form-label fw-bold">Nueva contraseña *</label>
          <input
            type="password"
            className="form-control"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label fw-bold">Confirmar contraseña *</label>
          <input
            type="password"
            className="form-control"
            value={confirmar}
            onChange={(e) => setConfirmar(e.target.value)}
            required
          />
        </div>

        {error && <div className="alert alert-danger py-1">{error}</div>}
        {mensaje && <div className="alert alert-success py-1">{mensaje}</div>}

        <div className="d-grid mb-2">
          <button className="btn btn-success">Cambiar contraseña</button>
        </div>
        <div className="text-center">
          <a href="/" className="custom-link">Iniciar sesión</a>
        </div>
      </form>
    )
  }

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
      <div className="card p-4 shadow" style={{ maxWidth: 400, width: '100%' }}>
        <div className="text-center mb-3">
          <img src={logo} alt="Logo" style={{ height: '60px' }} />
          <h5 className="brand-title fw-bold mt-2">TECHNOLOGICAL CONSULTING</h5>
        </div>
        {renderFormulario()}
      </div>
    </div>
  )
}
