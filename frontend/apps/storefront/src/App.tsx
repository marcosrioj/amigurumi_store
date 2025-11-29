import { useEffect, useState } from "react";
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

export function App() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      try {
        const response = await axios.get<Product[]>(`${apiConfig.catalog}/api/products`);
        setProducts(response.data);
      } catch (error) {
        console.error("Failed to load products", error);
      } finally {
        setLoading(false);
      }
    };

    void load();
  }, []);

  return (
    <div
      style={{
        minHeight: "100vh",
        padding: "2rem clamp(1rem, 2vw, 2.5rem)",
        maxWidth: "1200px",
        margin: "0 auto",
        display: "flex",
        flexDirection: "column",
        gap: "2rem"
      }}
    >
      <header style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <div>
          <div style={{ color: palette.highlight, fontSize: "0.85rem", letterSpacing: "0.15em" }}>
            AMIGURUMI STORE
          </div>
          <h1 style={{ margin: 0, color: palette.text, fontSize: "2.25rem" }}>Handcrafted plushies</h1>
          <p style={{ color: palette.muted, maxWidth: "520px" }}>
            Fresh drop of crochet characters, stitched with love. Sign in to start curating your cart.
          </p>
        </div>
        <div style={{ display: "flex", gap: "0.75rem", alignItems: "center" }}>
          <Button tone="ghost" onClick={() => window.open("mailto:hello@amigurumi.local", "_blank")}>
            Contact
          </Button>
          <Button onClick={() => alert("Auth wiring pending backend live run")}>Sign in</Button>
        </div>
      </header>

      {loading ? (
        <div style={{ color: palette.muted }}>Loading catalog...</div>
      ) : (
        <section
          style={{
            display: "grid",
            gap: "1rem",
            gridTemplateColumns: "repeat(auto-fill, minmax(240px, 1fr))"
          }}
        >
          {products.map((product) => (
            <Card
              key={product.id}
              title={product.name}
              footer={
                <>
                  <Button
                    onClick={() => alert("Ordering wire-up in progress")}
                  >{`Add • $${product.price.toFixed(2)}`}</Button>
                  <span style={{ color: palette.muted, fontSize: "0.85rem" }}>
                    {product.stock} in stock • {product.difficulty}
                  </span>
                </>
              }
            >
              <div style={{ marginBottom: "0.5rem" }}>{product.description}</div>
              <div style={{ fontSize: "0.85rem", color: palette.highlight }}>{product.yarnType}</div>
            </Card>
          ))}
        </section>
      )}
    </div>
  );
}
