module.exports = async ({ github, context }) => {
  const markerId = "e2e-test-status-report";
  const marker = `<!-- bot-comment-id: ${markerId} -->`;

  const platform = (process.env.PLATFORM || "").toLowerCase().trim();
  const testStatus = (process.env.TEST_STATUS || "").trim();
  const jobStatus = (process.env.JOB_STATUS || "").trim();
  const reportDir = (process.env.REPORT_DIR || "").trim();

  const isMobile = platform.includes("mobile");
  const isWebsite = platform.includes("website");

  if (!isMobile && !isWebsite) {
    console.log(`Unknown platform: ${platform}. Skipping comment.`);
    return;
  }

  function formatJobStatus(status, conclusion) {
    if (conclusion) {
      if (conclusion === "success") return "✅ Success";
      if (conclusion === "failure") return "❌ Failed";
      if (conclusion === "cancelled") return "⚠️ Cancelled";
      if (conclusion === "skipped") return "⏭️ Skipped";
      if (conclusion === "timed_out") return "⏰ Timed Out";
      if (conclusion === "action_required") return "🔘 Action Required";
      if (conclusion === "neutral") return "⚪ Neutral";
      return conclusion;
    }
    if (status) {
      if (status === "in_progress") return "⏳ In Progress";
      if (status === "queued") return "⏳ Queued";
      if (status === "waiting") return "⏳ Waiting";
      if (status === "requested") return "⏳ Requested";
      if (status === "pending") return "⏳ Pending";
      return status;
    }
    return "N/A";
  }

  function getStatus(stepOutcome, jobResult) {
    const effectiveOutcome = stepOutcome || jobResult;
    if (effectiveOutcome === "success") {
      return "✅ Success";
    } else if (effectiveOutcome === "failure") {
      return "❌ Failed";
    } else if (effectiveOutcome === "cancelled") {
      return "⚠️ Cancelled";
    } else if (effectiveOutcome === "skipped") {
      return "⏭️ Skipped";
    }
    return effectiveOutcome || "N/A";
  }

  function getReportLinkAndStatus(dir, isMob) {
    if (!dir || dir === "N/A") {
      return { reportLink: "N/A", reportStatus: "N/A" };
    }
    let rawUrl = "";
    if (isMob) {
      rawUrl = `https://carl-johnsons.github.io/tastopia/${dir}/report.html`;
    } else {
      rawUrl = `https://carl-johnsons.github.io/tastopia/${dir}/index.html`;
    }
    const encodedUrl = encodeURIComponent(rawUrl);
    const reportLink = `[View report](${rawUrl})`;
    const reportStatus = `![Report Status](https://img.shields.io/website?url=${encodedUrl}&up_message=Ready&down_message=Deploying...&up_color=brightgreen&down_color=yellow&label=Report)`;
    return { reportLink, reportStatus };
  }

  async function resolveIssueNumber() {
    if (context.issue && context.issue.number) {
      return context.issue.number;
    }
    if (context.payload.pull_request?.number) {
      return context.payload.pull_request.number;
    }

    const sha = context.sha;
    if (!sha) {
      return undefined;
    }

    try {
      const { data: prs } =
        await github.rest.repos.listPullRequestsAssociatedWithCommit({
          owner: context.repo.owner,
          repo: context.repo.repo,
          commit_sha: sha,
        });

      const openPrs = prs.filter((pr) => pr.state === "open");
      if (openPrs.length === 0) {
        return undefined;
      }

      const promotionPr = openPrs.find(
        (pr) => pr.base.ref === "master" && pr.head.ref === "staging"
      );

      return (promotionPr || openPrs[0]).number;
    } catch (err) {
      console.log("Could not resolve PR from commit SHA:", err.message);
      return undefined;
    }
  }

  const status = getStatus(testStatus, jobStatus);
  const { reportLink, reportStatus } = getReportLinkAndStatus(reportDir, isMobile);

  const displayName = isMobile ? "📱 Mobile" : "🌐 Website";
  const targetRow = `| ${displayName} | ${status} | ${reportStatus} | ${reportLink} |`;
  const rowRegex = isMobile
    ? /^\|\s*📱\s*Mobile\s*\|.*$/m
    : /^\|\s*🌐\s*Website\s*\|.*$/m;

  const otherDisplayName = isMobile ? "🌐 Website" : "📱 Mobile";
  const otherRowRegex = isMobile
    ? /^\|\s*🌐\s*Website\s*\|.*$/m
    : /^\|\s*📱\s*Mobile\s*\|.*$/m;

  let otherStatus = "⏳ In Progress";
  let otherReportStatus = "N/A";
  let otherReportLink = "N/A";
  let otherJob = null;

  try {
    const { data } = await github.rest.actions.listJobsForWorkflowRun({
      owner: context.repo.owner,
      repo: context.repo.repo,
      run_id: context.runId,
    });

    otherJob = data.jobs.find((j) =>
      isMobile
        ? j.name === "Website E2E tests"
        : j.name === "Mobile E2E tests"
    );

    if (otherJob) {
      otherStatus = formatJobStatus(otherJob.status, otherJob.conclusion);
    } else {
      otherStatus = "⏭️ Skipped";
    }
  } catch (err) {
    console.log("Could not query workflow run jobs:", err.message);
  }

  const defaultOtherRow = `| ${otherDisplayName} | ${otherStatus} | ${otherReportStatus} | ${otherReportLink} |`;
  const defaultMobile = isMobile ? targetRow : defaultOtherRow;
  const defaultWebsite = isWebsite ? targetRow : defaultOtherRow;

  const initialContent =
    `${marker}\n` +
    `### 🧪 E2E Test Report Summary\n\n` +
    `| Platform | Test Status | Report Deployment Status | Link |\n` +
    `|---|---|---|---|\n` +
    `${defaultMobile}\n` +
    `${defaultWebsite}\n`;

  const issueNumber = await resolveIssueNumber();

  if (!issueNumber) {
    console.log("No PR number found. Skipping PR comment.");
    return;
  }

  const comments = await github.paginate(github.rest.issues.listComments, {
    owner: context.repo.owner,
    repo: context.repo.repo,
    issue_number: issueNumber,
  });

  const botComment = comments.find(
    (comment) => comment.body && comment.body.includes(marker),
  );

  if (botComment) {
    let newContent = "";
    if (rowRegex.test(botComment.body)) {
      newContent = botComment.body.replace(rowRegex, targetRow);
      const isOtherPending =
        newContent.includes("⏳ In Progress") ||
        newContent.includes("⏳ Queued") ||
        newContent.includes("⏳ Waiting") ||
        newContent.includes("⏳ Pending") ||
        newContent.includes("⏳ Requested");
      if (otherJob && isOtherPending && otherJob.conclusion) {
        newContent = newContent.replace(otherRowRegex, defaultOtherRow);
      }
    } else {
      newContent = initialContent;
    }

    await github.rest.issues.updateComment({
      owner: context.repo.owner,
      repo: context.repo.repo,
      comment_id: botComment.id,
      body: newContent,
    });
    console.log(`Updated existing comment: ${botComment.id}`);
  } else {
    await github.rest.issues.createComment({
      owner: context.repo.owner,
      repo: context.repo.repo,
      issue_number: issueNumber,
      body: initialContent,
    });
    console.log("Created new comment!");
  }
};
