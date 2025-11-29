import { FormEvent, useEffect, useState, type CSSProperties } from "react";
import axios from "axios";
import { apiConfig } from "@amigurumi/config";
import { Button, Card, palette } from "@amigurumi/ui";

type Product = {
  id: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  yarnType: string;
  difficulty: string;
  imageUrl: string;
};

const blankProduct: Omit<Product, "id"> = {
  name: "",
  description: "",
  price: 0,
  stock: 0,
  yarnType: "",
  difficulty: "Beginner",
  imageUrl: ""
};

export function App() {
  const [products, setProducts] = useState<Product[]>([]);
  const [form, setForm] = useState(blankProduct);
  const [saving, setSaving] = useState(false);

  const load = async () => {
    const response = await axios.get<Product[]>(`${apiConfig.catalog}/api/products`);
    setProducts(response.data);
  };

  useEffect(() => {
    void load();
  }, []);

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setSaving(true);
    try {
      await axios.post(`${apiConfig.catalog}/api/products`, form);
      setForm(blankProduct);
      await load();
    } finally {
      setSaving(false);
    }
  };

  return (
    <div
      style={{
        minHeight: "100vh",
        padding: "2rem clamp(1rem, 2vw, 2.5rem)",
        maxWidth: "1100px",
        margin: "0 auto",
        display: "flex",
        flexDirection: "column",
        gap: "1.5rem"
      }}
    >
      <header style={{ display: "flex", alignItems: "center", gap: "1rem", justifyContent: "space-between" }}>
        <div>
          <div style={{ color: palette.accent, fontSize: "0.85rem", letterSpacing: "0.1em" }}>ADMIN</div>
          <h1 style={{ margin: 0, color: palette.text }}>Amigurumi control room</h1>
          <p style={{ color: palette.muted }}>Manage catalog inventory and feature drops.</p>
        </div>
        <Button onClick={() => void load()}>Refresh</Button>
      </header>

      <Card title="Create product">
        <form
          onSubmit={handleSubmit}
          style={{ display: "grid", gap: "0.75rem", gridTemplateColumns: "repeat(auto-fit, minmax(240px, 1fr))" }}
        >
          <label style={{ display: "flex", flexDirection: "column", gap: "0.35rem", color: palette.text }}>
            Name
            <input
              required
              value={form.name}
              onChange={(e) => setForm({ ...form, name: e.target.value })}
              style={inputStyle}
              placeholder="Sky Whale"
            />
          </label>
          <label style={{ display: "flex", flexDirection: "column", gap: "0.35rem", color: palette.text }}>
            Price
            <input
              type="number"
              required
              step="0.01"
              value={form.price}
              onChange={(e) => setForm({ ...form, price: Number(e.target.value) })}
              style={inputStyle}
              placeholder="29.50"
            />
          </label>
          <label style={{ display: "flex", flexDirection: "column", gap: "0.35rem", color: palette.text }}>
            Stock
            <input
              type="number"
              required
              value={form.stock}
              onChange={(e) => setForm({ ...form, stock: Number(e.target.value) })}
              style={inputStyle}
              placeholder="18"
            />
          </label>
          <label style={{ display: "flex", flexDirection: "column", gap: "0.35rem", color: palette.text }}>
            Yarn type
            <input
              value={form.yarnType}
              onChange={(e) => setForm({ ...form, yarnType: e.target.value })}
              style={inputStyle}
              placeholder="Velvet bulky"
            />
          </label>
          <label style={{ display: "flex", flexDirection: "column", gap: "0.35rem", color: palette.text }}>
            Difficulty
            <select
              value={form.difficulty}
              onChange={(e) => setForm({ ...form, difficulty: e.target.value })}
              style={{ ...inputStyle, color: palette.text }}
            >
              <option value="Beginner">Beginner</option>
              <option value="Intermediate">Intermediate</option>
              <option value="Advanced">Advanced</option>
            </select>
          </label>
          <label style={{ display: "flex", flexDirection: "column", gap: "0.35rem", color: palette.text }}>
            Image URL
            <input
              value={form.imageUrl}
              onChange={(e) => setForm({ ...form, imageUrl: e.target.value })}
              style={inputStyle}
              placeholder="/images/sky-whale.jpg"
            />
          </label>
          <label
            style={{
              display: "flex",
              flexDirection: "column",
              gap: "0.35rem",
              color: palette.text,
              gridColumn: "1 / -1"
            }}
          >
            Description
            <textarea
              required
              rows={3}
              value={form.description}
              onChange={(e) => setForm({ ...form, description: e.target.value })}
              style={{ ...inputStyle, resize: "vertical" }}
              placeholder="Plush whale with cloud appliques."
            />
          </label>
          <div style={{ gridColumn: "1 / -1" }}>
            <Button type="submit" disabled={saving}>
              {saving ? "Saving..." : "Publish product"}
            </Button>
          </div>
        </form>
      </Card>

      <Card title="Inventory">
        <div style={{ display: "grid", gap: "0.75rem" }}>
          {products.map((product) => (
            <div
              key={product.id}
              style={{
                display: "grid",
                gridTemplateColumns: "1fr auto",
                alignItems: "center",
                padding: "0.75rem 1rem",
                borderRadius: "12px",
                background: "rgba(255,255,255,0.02)"
              }}
            >
              <div>
                <div style={{ fontWeight: 700 }}>{product.name}</div>
                <div style={{ color: palette.muted, fontSize: "0.9rem" }}>{product.description}</div>
              </div>
              <div style={{ textAlign: "right", color: palette.highlight }}>
                ${product.price.toFixed(2)} · {product.stock} units
              </div>
            </div>
          ))}
        </div>
      </Card>
    </div>
  );
}

const inputStyle: CSSProperties = {
  background: "rgba(255,255,255,0.04)",
  border: "1px solid rgba(255,255,255,0.08)",
  borderRadius: "12px",
  padding: "0.65rem 0.85rem",
  color: palette.text,
  fontSize: "1rem"
};
