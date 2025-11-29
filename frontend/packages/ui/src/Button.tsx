import type { ButtonHTMLAttributes, CSSProperties, ReactNode } from "react";
import { palette, radius } from "./theme";

type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  children: ReactNode;
  tone?: "primary" | "ghost";
};

export function Button({ children, tone = "primary", ...props }: ButtonProps) {
  const baseStyle: CSSProperties = {
    display: "inline-flex",
    alignItems: "center",
    justifyContent: "center",
    gap: "0.5rem",
    padding: "0.75rem 1.25rem",
    borderRadius: radius.md,
    border: "1px solid transparent",
    cursor: "pointer",
    fontWeight: 700,
    letterSpacing: "0.01em",
    transition: "all 150ms ease",
    color: palette.text
  };

  const toneStyles: Record<ButtonProps["tone"], CSSProperties> = {
    primary: {
      background: `linear-gradient(120deg, ${palette.highlight}, ${palette.accent})`,
      boxShadow: "0 10px 30px rgba(0,0,0,0.3)"
    },
    ghost: {
      background: "transparent",
      borderColor: palette.highlight,
      color: palette.highlight
    }
  };

  return (
    <button style={{ ...baseStyle, ...toneStyles[tone] }} {...props}>
      {children}
    </button>
  );
}
