import cortarLabel from "src/directives/cortar-label";
import focus from "src/directives/focus";
import permitido from "src/directives/permitido";
import print from "vue3-print-nb";

export default ({ app }) => {
  // 'my-directive' will be used as 'v-my-directive'
  app.directive("cortarLabel", cortarLabel);
  app.directive("focus", focus);
  app.directive("permitido", permitido);
  app.directive("print", print);
};
