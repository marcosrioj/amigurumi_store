import type { ReactNode } from "react";
import { palette, radius } from "./theme";

type CardProps = {
  title?: string;
  children: ReactNode;
  footer?: ReactNode;
};

export function Card({ title, children, footer }: CardProps) {
  return (
    <article
      style={{
        background: palette.surface,
        borderRadius: radius.lg,
        padding: "1.5rem",
        border: `1px solid rgba(255,255,255,0.08)`,
        boxShadow: "0 15px 40px rgba(0,0,0,0.35)"
      }}
    >
      {title ? (
        <header
          style={{
            marginBottom: "0.75rem",
            color: palette.text,
            fontWeight: 700,
            letterSpacing: "0.02em"
          }}
        >
          {title}
        </header>
      ) : null}
      <div style={{ color: palette.muted }}>{children}</div>
      {footer ? (
        <footer style={{ marginTop: "1rem" }}>
          <div style={{ display: "flex", gap: "0.75rem", flexWrap: "wrap" }}>{footer}</div>
        </footer>
      ) : null}
    </article>
  );
}
