export function FormHeader({ title, description }: { title: string; description: string }) {
	return (
		<div className="form-header">
			<div>
				<p className="eyebrow">REQUISIÇÃO</p>
				<h2>{title}</h2>
				<p className="form-description">{description}</p>
			</div>
			<span className="required-indicator">* Obrigatório</span>
		</div>
	);
}

export function SubmitButton({ isLoading, children = 'Enviar chamada' }: { isLoading: boolean; children?: string }) {
	return (
		<button className="submit-button" type="submit" disabled={isLoading}>
			{isLoading ? 'Enviando…' : children}
			<span aria-hidden="true">{isLoading ? '· · ·' : '→'}</span>
		</button>
	);
}