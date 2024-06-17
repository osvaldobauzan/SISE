import { ref } from "vue";

export class AutoridadJudicial {
  empleadoId = ref(0);
  nombreCompleto = ref("");
  nombreOficial = ref("");
  cargoDescripcion = ref("");
  catOrganismoId = ref("");
}

// {
//   "nombreCompleto": "Juan Perez Torres",
//   "empleadoId": 108032,
//   "cargoDescripcion": "Secretario de Tribunal",
//   "catOrganismoId": 4385,
//   "nombreOficial": "Pleno Regional en Materia Penal de la Región Centro-Norte, con residencia en la Ciudad de México"
// },
