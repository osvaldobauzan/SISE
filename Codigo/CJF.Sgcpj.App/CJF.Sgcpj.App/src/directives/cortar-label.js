const fontSize = 7.1;

const eliminarTooltipExistente = (el) => {
  const tooltipExistente = el.querySelector(".custom-tooltip");
  if (tooltipExistente) {
    tooltipExistente.remove();
  }
};

const crearTooltip = (el) => {
  eliminarTooltipExistente(el);
  const tooltip = document.createElement("div");
  tooltip.classList.add("custom-tooltip");
  tooltip.innerText = el.dataset.originalText;
  tooltip.style.position = "absolute";
  tooltip.style.backgroundColor = "#757575";
  tooltip.style.color = "white";
  tooltip.style.padding = "5px";
  tooltip.style.borderRadius = "5px";
  tooltip.style.display = "none";
  tooltip.style.zIndex = "1000";
  tooltip.style.whiteSpace = "nowrap";
  tooltip.style.pointerEvents = "none";
  el.appendChild(tooltip);
  return tooltip;
};

const actualizarVisibilidadTooltip = (el, tooltip, esVisible) => {
  tooltip.style.display = esVisible ? "block" : "none";
};

const aplicarTooltip = (el) => {
  const input = el.querySelector("input");
  input.style.position = "absolute";
  input.style.padding = "0px";
  input.style.width = "100%";
  const span = el.querySelector("span");

  if (span) {
    const originalText = span.innerText;
    const isOverflowing = originalText.length > el.offsetWidth / fontSize;
    if (!span.innerText.includes("..."))
      el.dataset.originalText = originalText.slice();
    if (isOverflowing) {
      let tooltip = crearTooltip(el);
      const truncatedText =
        originalText.substring(0, Math.round(el.offsetWidth / fontSize) - 3) +
        "...";
      span.innerText = truncatedText;

      el.addEventListener("mouseover", () => {
        const tooltipExistente = el.querySelector(".custom-tooltip");
        if (!tooltipExistente) {
          tooltip.innerText = el.dataset.originalText;
          el.appendChild(tooltip);
        }
        actualizarVisibilidadTooltip(
          el,
          tooltip,
          span.innerText.includes("..."),
        );
      });
      el.addEventListener("mouseleave", () => {
        actualizarVisibilidadTooltip(el, tooltip, false);
        eliminarTooltipExistente(el);
      });
    } else {
      span.innerText = originalText;
      eliminarTooltipExistente(el);
    }
  }
};

export default {
  mounted(el) {
    aplicarTooltip(el);
  },
  updated(el) {
    aplicarTooltip(el);
  },
};
