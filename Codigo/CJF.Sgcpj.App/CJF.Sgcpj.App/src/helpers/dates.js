import { date } from "quasar";

const replaceDateFormat = (
  dateToChange,
  patternOriginalDate,
  newPatterDate,
) => {
  if (!dateToChange) return;
  return date.formatDate(
    date.extractDate(dateToChange, patternOriginalDate),
    newPatterDate,
    {
      days: "Domingo_Lunes_Martes_Miércoles_Jueves_Viernes_Sábado".split("_"),
      daysShort: "Dom_Lun_Mar_Mié_Jue_Vie_Sáb".split("_"),
      months:
        "Enero_Febrero_Marzo_Abril_Mayo_Junio_Julio_Agosto_Septiembre_Octubre_Noviembre_Diciembre".split(
          "_",
        ),
      monthsShort: "Ene_Feb_Mar_Abr_May_Jun_Jul_Ago_Sep_Oct_Nov_Dic".split("_"),
    },
  );
};

export const customDates = {
  replaceDateFormat: (dateToChange, patternOriginalDate, newPatterDate) =>
    replaceDateFormat(dateToChange, patternOriginalDate, newPatterDate),
};
