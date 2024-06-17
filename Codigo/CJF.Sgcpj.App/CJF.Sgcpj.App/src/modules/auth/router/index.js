export default {
  meta: { requiresAuth: true },
  component: () => import("src/modules/auth/layouts/PlecaLayout.vue"),
  children: [
    {
      path: "login",
      name: "login",
      component: () => import("src/modules/auth/pages/LoginPage.vue"),
      props: (route) => ({
        showBanner: !!route.params.showBanner,
      }),
    },
  ],
};
