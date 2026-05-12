export type KidStats = {
  completedThisWeek: number;
  approvalRate: number;
};

export const DEFAULT_KID_STATS = Object.freeze<KidStats>({
  completedThisWeek: 0,
  approvalRate: 0,
});
