/* 
 * 
 * (c) Copyright Ascensio System Limited 2010-2014
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 * 
 * http://www.gnu.org/licenses/agpl.html 
 * 
 */

using System;
using ASC.Notify.Model;
using ASC.Notify.Patterns;
using NotifySourceBase = ASC.Core.Notify.NotifySource;

namespace ASC.Projects.Core.Services.NotifyService
{
    public class NotifySource : NotifySourceBase
    {
        public static NotifySource Instance
        {
            get;
            private set;
        }


        static NotifySource()
        {
            Instance = new NotifySource();
        }

        private NotifySource()
            : base(new Guid("{6045B68C-2C2E-42db-9E53-C272E814C4AD}"))
        {
        }


        protected override IActionProvider CreateActionProvider()
        {
            return new ConstActionProvider(
                NotifyConstants.Event_NewCommentForMessage,
                NotifyConstants.Event_NewCommentForTask,
                NotifyConstants.Event_NewFileForDiscussion,
                NotifyConstants.Event_NewFileForTask,
                NotifyConstants.Event_ResponsibleForProject,
                NotifyConstants.Event_ResponsibleForTask,
                NotifyConstants.Event_RemoveResponsibleForTask,
                NotifyConstants.Event_ReminderAboutTask,
                NotifyConstants.Event_ReminderAboutTaskDeadline,
                NotifyConstants.Event_TaskEdited,
                NotifyConstants.Event_TaskClosed,
                NotifyConstants.Event_TaskCreated,
                NotifyConstants.Event_TaskResumed,
                NotifyConstants.Event_TaskDeleted,
                NotifyConstants.Event_TaskMovedToMilestone,
                NotifyConstants.Event_TaskMovedFromMilestone,
                NotifyConstants.Event_InviteToProject,
                NotifyConstants.Event_RemoveFromProject,
                NotifyConstants.Event_MilestoneDeadline,
                NotifyConstants.Event_MilestoneEdited,
                NotifyConstants.Event_MilestoneClosed,
                NotifyConstants.Event_MilestoneCreated,
                NotifyConstants.Event_MilestoneDeleted,
                NotifyConstants.Event_MilestoneResumed,
                NotifyConstants.Event_ResponsibleForMilestone,
                NotifyConstants.Event_MessageCreated,
                NotifyConstants.Event_MessageEdited,
                NotifyConstants.Event_MessageDeleted,
                NotifyConstants.Event_ImportData,
                NotifyConstants.Event_ResponsibleForSubTask,
                NotifyConstants.Event_SubTaskDeleted,
                NotifyConstants.Event_SubTaskCreated,
                NotifyConstants.Event_SubTaskEdited,
                NotifyConstants.Event_SubTaskClosed,
                NotifyConstants.Event_SubTaskResumed,
                NotifyConstants.Event_ProjectDeleted,
                NotifyConstants.Event_EditedCommentForMessage,
                NotifyConstants.Event_EditedCommentForTask);
        }

        protected override IPatternProvider CreatePatternsProvider()
        {
            return new XmlPatternProvider2(Resources.ProjectsPatternResource.patterns) { GetPatternMethod = ChoosePattern };
        }

        private IPattern ChoosePattern(INotifyAction action, string senderName, Notify.Engine.NotifyRequest request)
        {
            if (action == NotifyConstants.Event_NewCommentForMessage)
            {
                var tag = request.Arguments.Find(tv => tv.Tag == "EventType");
                if (tag != null)
                {
                    return PatternProvider.GetPattern(new NotifyAction(Convert.ToString(tag.Value)), senderName);
                }
            }
            return null;
        }
    }
}
