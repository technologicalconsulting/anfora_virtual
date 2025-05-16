import { useState } from "react";
import {
  sendCorreo,
  verifyCode,
  resetPassword,
} from "../../services/authService";
import bg from "../../assets/images/bg-login.jpg";
import logo from "../../assets/images/logo.png";
import { useNavigate } from "react-router-dom";

export default function ResetPasswordPage() {
  const navigate = useNavigate();
  const [fase, setFase] = useState<"inicio" | "verificacion" | "nueva-clave">(
    "inicio"
  );
  const [correo, setCorreo] = useState("");
  const [codigo, setCodigo] = useState("");
  const [codigoVerificado, setCodigoVerificado] = useState("");
  const [password, setPassword] = useState("");
  const [confirmar, setConfirmar] = useState("");
  const [mensaje, setMensaje] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const handleApiError = (
    error: any,
    propiedad: string,
    setError: (msg: string) => void
  ) => {
    if (error.response?.status === 400) {
      const dtoErrors = error.response.data.errors?.[propiedad];
      if (dtoErrors) {
        setError(dtoErrors.join(" "));
        return;
      }
    }
    if (error.response?.data?.title) {
      setError(error.response.data.title);
    } else if (typeof error.response?.data === "string") {
      setError(error.response.data);
    } else {
      setError("Ocurrió un error inesperado");
    }
  };

  const handleVerificarCorreo = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setMensaje("");
    try {
      setLoading(true);
      await sendCorreo(correo);
      setMensaje("Código enviado correctamente al correo ingresado");
      setFase("verificacion");
    } catch (error: any) {
      handleApiError(error, "Email", setError);
    } finally {
      setLoading(false);
    }
  };

  const handleVerificarCodigo = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setMensaje("");
    try {
      setLoading(true);
      const response = await verifyCode(codigo);
      setCodigoVerificado(response.data.code);
      setMensaje("Codigo verificado correctamente");
      setFase("nueva-clave");
    } catch (error: any) {
      handleApiError(error, "Code", setError);
    } finally {
      setLoading(false);
    }
  };

  const handleCambiarClave = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setMensaje("");
    if (password !== confirmar) {
      setError("Las contraseñas no coinciden");
      return;
    }
    try {
      setLoading(true);
      await resetPassword(codigoVerificado, password);
      setMensaje("Contraseña actualizada exitosamente");
      setTimeout(() => {
        navigate("/", { replace: true });
      }, 3000);
      setFase("inicio");
      setCorreo("");
      setCodigo("");
      setCodigoVerificado("");
      setPassword("");
      setConfirmar("");
    } catch (error: any) {
      handleApiError(error, "NewPassword", setError);
    } finally {
      setLoading(false);
    }
  };

  const renderFormulario = () => {
    if (fase === "inicio" || fase === "verificacion") {
      return (
        <form
          onSubmit={
            fase === "inicio" ? handleVerificarCorreo : handleVerificarCodigo
          }
        >
          <div className="mb-3">
            <label className="form-label fw-bold">Correo Electronico*</label>
            <input
              type="text"
              className="form-control"
              value={correo}
              onChange={(e) => setCorreo(e.target.value)}
              required
              disabled={fase === "verificacion"}
            />
          </div>

          {fase === "verificacion" && (
            <div className="mb-3">
              <label className="form-label fw-bold">
                Código de verificación *
              </label>
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

          {loading ? (
            <div className="d-flex justify-content-center">
              <div className="spinner-border text-primary" role="status"></div>
            </div>
          ) : (
            <div className="d-grid mb-2">
              <button className="btn btn-success">
                {fase === "inicio" ? "Verificar" : "Completar Registro"}
              </button>
            </div>
          )}

          <div className="text-center">
            <span>¿Tienes una cuenta? </span>
            <a href="/" className="custom-link">
              Iniciar sesión
            </a>
          </div>
        </form>
      );
    }
  };

  const resetPasswordFormulario = () => {
    return (
      <form onSubmit={handleCambiarClave}>
        <div className="mb-3">
          <label className="form-label fw-bold">Nueva contraseña *</label>
          <label className="form-label small text-muted">
            <li>La contraseña debe tener al menos 8 caracteres</li>
          </label>
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
          <a href="/" className="custom-link">
            Iniciar sesión
          </a>
        </div>
      </form>
    );
  };

  return (
    <div
      className="d-flex justify-content-center align-items-center"
      style={{
        minHeight: "100vh",
        width: "100vw",
        backgroundImage: `url(${bg})`,
        backgroundSize: "cover",
        backgroundPosition: "center",
        backgroundRepeat: "no-repeat",
      }}
    >
      <div className="card p-4 shadow" style={{ maxWidth: 400, width: "100%" }}>
        <div className="text-center mb-3">
          <img src={logo} alt="Logo" style={{ height: "60px" }} />
          <h5 className="brand-title fw-bold mt-2">TECHNOLOGICAL CONSULTING</h5>
        </div>
        {fase === "nueva-clave"
          ? resetPasswordFormulario()
          : renderFormulario()}
      </div>
    </div>
  );
}
