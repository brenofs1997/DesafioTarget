import type { OperationDetails } from '../types/operations';

export function PageHeader({ operation }: { operation: OperationDetails }) {
	return (
		<>
			<header className="topbar">
				<div className="breadcrumb">
					<span>{operation.group}</span><span className="breadcrumb-separator">/</span><strong>{operation.label}</strong>
				</div>
				<div className="api-indicator"><span className="connection-dot" /> localhost:8080</div>
			</header>
			<section className="page-heading">
				<div>
					<p className="eyebrow">{operation.group} <span>·</span> API</p>
					<h1>{operation.title}</h1>
					<p className="page-intro">{operation.intro}</p>
				</div>
				<div className="route-badge">
					<span className={`method method-${operation.method.toLowerCase()}`}>{operation.method}</span>
					<code>{operation.route}</code>
				</div>
			</section>
		</>
	);
}